using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)//Cancellationtoken tarayıcı kapanırsa otomatik iptal eder
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", photo.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream, cancellationToken);
                }

                var returnPath = "Photos/" + photo.FileName;


                return Ok("Url = " + returnPath);
            }
            return BadRequest("Photo is empty");
        }

        [HttpGet("deletephoto")]
        public IActionResult PhotoDelete(string photoName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", photoName);
            if (!System.IO.File.Exists(path))
            {
                return BadRequest("Photo not found");
            }

            System.IO.File.Delete(path);
            return NoContent();
        }

    }
}
