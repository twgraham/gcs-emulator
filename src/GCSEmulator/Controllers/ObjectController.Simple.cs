using System.Threading.Tasks;
using GCSEmulator.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GCSEmulator.Controllers
{
    public partial class ObjectController
    {
        private async Task<IActionResult> UploadMedia(string name)
        {
            Request.EnableBuffering();
            var storageObject = new StorageObject(name);
            await _session.StoreAsync(storageObject);
            _session.Advanced.Attachments.Store(storageObject, "data", Request.Body, Request.ContentType);
            await _session.SaveChangesAsync();

            return Created("", storageObject);
        }
    }
}