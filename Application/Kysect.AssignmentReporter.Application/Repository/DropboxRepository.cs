﻿using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;
using Kysect.AssignmentReporter.Domain;
using Kysect.AssignmentReporter.Dto;
using Microsoft.Extensions.Configuration;

namespace Kysect.AssignmentReporter.Application;

public class DropboxRepository : IRepository
{
    //TODO: read config on startup
    private readonly IConfiguration _configuration;

    public DropboxRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<FileEntry> Save(Stream stream)
    {
        string name = Path.GetRandomFileName();
        while (CheckName(name))
        {
            name = Path.GetRandomFileName();
        }

        await WriteFile(new FileDto(name, stream));
        return new FileEntry(name);
    }

    public async Task<FileDto> Get(Report report)
    {
        byte[] files = await GetFile(report.File.FullName);
        return new FileDto(report.FileName, new MemoryStream(files));
    }

    public async Task Delete(Report report)
    {
        await Delete(report.FileName);
    }

    private async Task Delete(string path)
    {
        string accessToken = _configuration.GetSection("DropBoxAccessToken").Value;
        using (var dropBox = new DropboxClient(accessToken))
        {
            var deleteArg = new DeleteArg(path);
            await dropBox.Files.DeleteV2Async(deleteArg); // TODO: handle exception
        }
    }

    private async Task<byte[]> GetFile(string file)
    {
        string accessToken = _configuration.GetSection("DropBoxAccessToken").Value;

        using (var dropBox = new DropboxClient(accessToken))
        using (IDownloadResponse<FileMetadata>? response = await dropBox.Files.DownloadAsync("/" + file))
        {
            return await response.GetContentAsByteArrayAsync();
        }
    }

    private async Task WriteFile(FileDto fileDto)
    {
        fileDto.Stream.Position = 0;
        string accessToken = _configuration.GetSection("DropBoxAccessToken").Value;
        using (var dropBox = new DropboxClient(accessToken))
        {
            await dropBox.Files.UploadAsync(
                "/" + fileDto.Name,
                WriteMode.Add.Instance,
                body: fileDto.Stream);
        }
    }

    private bool CheckName(string fileName)
    {
        string accessToken = _configuration.GetSection("DropBoxAccessToken").Value;
        using (var dropBox = new DropboxClient(accessToken))
        {
            Task<ListFolderResult> files = dropBox.Files.ListFolderAsync(string.Empty);
            return files.Result.Entries.Any(x => x.Name == fileName);
        }
    }
}