namespace Application.Storage;

public interface IBlobService
{
	Task<Guid> UploadAsync(Stream stream, string contentType);
	Task<(Stream, string)> DownloadAsync(Guid fileId);
	Task DeleteAsync(Guid fileId);
}
