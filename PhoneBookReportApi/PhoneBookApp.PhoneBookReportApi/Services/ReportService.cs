using AutoMapper;
using MongoDB.Driver;
using PhoneBookApp.PhoneBookReportApi.Dtos;
//using PhoneBookApp.PhoneBookReportApi.Enums;
using PhoneBookApp.PhoneBookReportApi.Models;
using PhoneBookApp.PhoneBookReportApi.Settings;
using Mass = MassTransit;
using PhoneBookApp.Shared.Dtos;
using PhoneBookApp.Shared.Messages;
using PhoneBookApp.Shared.Enums;

namespace PhoneBookApp.PhoneBookReportApi.Services
{
    public class ReportService: IReportService
    {
        private readonly IMongoCollection<Report> _reportCollection;
        private readonly IMongoCollection<ReportDetail> _reportDetailCollection;
        private readonly IMapper _mapper;
        private readonly Mass.IPublishEndpoint _publishEndpoint;
        public ReportService(IDatabaseSettings databaseSettings, IMapper mapper, Mass.IPublishEndpoint publishEndpoint)
        {

            var client = new MongoClient(databaseSettings.ConnectionString);
            var db = client.GetDatabase(databaseSettings.DatabaseName);
            _reportCollection = db.GetCollection<Report>(databaseSettings.ReportCollectionName);
            _reportDetailCollection = db.GetCollection<ReportDetail>(databaseSettings.ReportDetailCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Response<List<ReportDto>>> GetAllReportsAsync()
        {

            var reports = await _reportCollection.Find(c => true).ToListAsync();

            return Response<List<ReportDto>>.Success(_mapper.Map<List<ReportDto>>(reports), 200);

        }
        public async Task<Response<ReportCreateDto>> CreateReportAsync()
        {           

            var reportCreateDto = new ReportCreateDto() { CreatedDate=DateTime.UtcNow , Status= ReportStatus.Preparing};

            var report = _mapper.Map<Report>(reportCreateDto);

            await _reportCollection.InsertOneAsync(report);

            await _publishEndpoint.Publish<CreateReportEvent>(new CreateReportEvent { Id = report.Id, CreatedDate = report.CreatedDate, Status = ReportStatus.Preparing });


            return Response<ReportCreateDto>.Success(_mapper.Map<ReportCreateDto>(report), 200);

        }

        public async Task<Report> GetReportByIdAsync(string id)
        {
            return await _reportCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<ReportDetail>> GetDetailsByReportIdAsync(string reportId)
        {
            return await _reportDetailCollection.Find(x => x.ReportId == reportId).ToListAsync();
        }

        public async Task CreateReportDetailsAsync(IList<ReportDetail> reportDetails)
        {
            await _reportDetailCollection.InsertManyAsync(reportDetails);

        }

        public async Task ReportCompletedAsync(string id)
        {
            var filter = Builders<Report>.Filter.Eq(s => s.Id, id);
            var update = Builders<Report>.Update
              .Set(s => s.Status, ReportStatus.Completed)
              .Set(s => s.CompletedDate, DateTime.UtcNow);
            await _reportCollection.UpdateOneAsync(filter, update);
        }
    }
}
