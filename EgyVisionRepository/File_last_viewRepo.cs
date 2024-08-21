using System.Linq;
using EgyVisionCore.Entities.EgyVision;
using EgyVisionRepository;

namespace EgyVisionService.EgyVision
{
	public interface IFile_last_viewRepo
	{
		IQueryable<File_last_view> Search();
		bool Insert(File_last_view model);
		bool Update(File_last_view model);
		bool Delete(File_last_view model);
		File_last_view GetById(string userId,long fileId);
	}

	public class File_last_viewRepo : IFile_last_viewRepo
	{
		private IEgyVisionRepository<File_last_view> _dbContext;
		public File_last_viewRepo(IEgyVisionRepository<File_last_view> dbContext)
		{
			_dbContext = dbContext;
		}

		public bool Insert(File_last_view model)
		{
			return _dbContext.Insert(model);
		}

		public bool Update(File_last_view model)
		{
			return _dbContext.Update(model);
		}

		public bool Delete(File_last_view model)
		{
			return _dbContext.Delete(model);
		}

		public IQueryable<File_last_view> Search()
		{
			return _dbContext.Table;
		}

		public File_last_view GetById(string userId,long fileId)
		{
			return _dbContext.Table.Where(x=>x.userId == userId && x.fileId == fileId ).FirstOrDefault();
		}

	}
}
