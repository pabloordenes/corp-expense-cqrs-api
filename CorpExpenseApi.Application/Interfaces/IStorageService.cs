using System;
using System.Collections.Generic;
using System.Text;

namespace CorpExpenseApi.Application.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadReceiptAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default);
    }
}
