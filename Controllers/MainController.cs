using kolos_apbd.DB;
using kolos_apbd.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolos_apbd.Controllers
{
    [ApiController]
    [Route("[main]")]
    public class MainController : Controller
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            id = Math.Abs(id);
            try
            {
                DbHandler handler = DbHandler.Instance();
                if (!await handler.AlbumExists(id)) return NotFound("Klient o podanym id nie istnieje");
                Album album = await handler.GetAlbum(id);
                IEnumerable<Track> tracks = await handler.GetTracks(album.IdAlbum);
                if (tracks.Count() == 0)
                {
                    return NotFound("Album znaleziono ale nie znaleziono piosenek");
                }

                return Ok(tracks);
            }
            catch (System.Exception)
            {
                return Conflict();
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusician(int id)
        {
            id = Math.Abs(id);
            try
            {
                DbHandler handler = DbHandler.Instance();
                if (!await handler.MusicianExists(id)) return NotFound("Muzyka nie znaleziono");

                /*
                 * todo: check if can be deleted
                 * Muzyka można usunąć tylko jeżeli bierze udział w tworzeniu utworów, 
                 * które nie pojawiły się jeszcze na docelowych albumach.
                 * */
                /*
                if(!await handler.MusicianCanBeDeleted(id))){

                }
                */

                if (!await handler.DeleteMusician(id))
                {
                    return Problem();
                }

                // todo: remove musician tracks

                return Ok(true);
            }
            catch (System.Exception)
            {
                return Conflict();
            }

        }
    }
}
