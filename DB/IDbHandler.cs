using kolos_apbd.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace kolos_apbd.DB
{
    public interface IDbHandler
    {
        static  DbHandler Instance() => throw new NotImplementedException();
        Task<bool> AlbumExists(int id);
        Task<Album> GetAlbum(int id);
        static Album AlbumFromValues(NameValueCollection values) => throw new NotImplementedException();
        static Track TrackFromValues(NameValueCollection values) => throw new NotImplementedException();
        Task<IEnumerable<Track>> GetTracks(int idAlbum);
        Task<bool> DeleteMusician(int id);
        Task<bool> MusicianExists(int id);

    }
}
