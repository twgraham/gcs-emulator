using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace GCSEmulator.Controllers
{
    [Route("/b/{bucketName}/o")]
    public partial class ObjectController : ControllerBase
    {
        private readonly IAsyncDocumentSession _session;

        [FromRoute(Name = "bucketName")]
        public string BucketName { get; set; }

        public enum UploadType
        {
            Media,
            Multipart,
            Resumable
        }

        public ObjectController(IAsyncDocumentSession session)
        {
            _session = session;
        }

        [HttpPost]
        public Task<IActionResult> UploadObject([FromQuery, Required] string name, [FromQuery, Required] UploadType uploadType)
        {
            return uploadType switch
            {
                UploadType.Media => UploadMedia(name),
                UploadType.Multipart => UploadMultipart(),
                UploadType.Resumable => InitiateResumable(name),
                _ => throw new ArgumentOutOfRangeException(nameof(uploadType), uploadType, null)
            };
        }
    }
}