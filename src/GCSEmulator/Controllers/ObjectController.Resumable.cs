using System;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using GCSEmulator.Data.Models;
using GCSEmulator.Dtos.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Raven.Client.Documents.Operations.Attachments;

namespace GCSEmulator.Controllers
{
    public partial class ObjectController
    {
        [HttpPut("{name}")]
        public async Task<IActionResult> UploadResumablePart()
        {
            Request.EnableBuffering();
            // await _session.Advanced.Attachments.Store()
            throw new NotImplementedException();
        }

        private async Task FinaliseResumableUpload(StorageObject storageObject)
        {
            var pipe = new Pipe();
            ReadPartsToPipe(storageObject, pipe.Writer);
            _session.Advanced.Attachments.Store(storageObject, "data", pipe.Reader.AsStream());

            await _session.SaveChangesAsync();
        }

        private async Task ReadPartsToPipe(StorageObject storageObject, PipeWriter pipeWriter)
        {
            var attachments = _session.Advanced.Attachments.GetNames(storageObject);
            var memory = pipeWriter.GetMemory();

            foreach (var attachment in attachments)
            {
                using var data = await _session.Advanced.Attachments.GetAsync(storageObject, attachment.Name);
                await data.Stream.WriteAsync(memory);
                pipeWriter.Advance((int)data.Details.Size);
                await pipeWriter.FlushAsync();
            }

            await pipeWriter.CompleteAsync();
        }

        private async Task<IActionResult> InitiateResumable(string name)
        {
            var newObjectDto = new NewObjectResourceDto();
            var result = await TryUpdateModelAsync(newObjectDto);
            Request.EnableBuffering();
            var storageObject = new StorageObject(name)
            {
                Metadata = newObjectDto.Metadata
            };
            await _session.StoreAsync(storageObject);
            await _session.SaveChangesAsync();

            Response.Headers.Add(HeaderNames.Location, "");
            return Ok();
        }
    }
}