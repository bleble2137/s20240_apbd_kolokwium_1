using kolos_apbd.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace kolos_apbd.DB
{
    public class DbHandler : IDbHandler
    {
        private static DbHandler _instance;
        public SqlConnection connection;

        private DbHandler()
        {
            connection = new SqlConnection("Data Source=cw4.sqlite");
        }

        public static DbHandler Instance()
        {
            if (_instance == null)
            {
                _instance = new DbHandler();
            }
            return _instance;
        }

        public async Task<bool> AlbumExists(int id)
        {
            Album album = null;
            try
            {
                this.connection.Open();
                SqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Album WHERE IdAlbum=@id";

                cmd.Parameters.Add(new SqlParameter("@id", id));

                int counter = (int)cmd.ExecuteScalar();
                if (counter > 0)
                {
                    return true;
                }
            }
            finally
            {
                this.connection.Close();
            }

            return false;
        }
        public async Task<Album> GetAlbum(int id)
        {
            Album album = null;
            try
            {
                this.connection.Open();
                SqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "SELECT 1 FROM Album WHERE IdAlbum=@id";

                cmd.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // no loop, we want only one record
                    NameValueCollection values = new NameValueCollection();
                    values.Add("IdAlbum", (string)reader["IdAlbum"]);
                    values.Add("AlbumName", (string)reader["AlbumName"]);
                    values.Add("PublishDate", (string)reader["PublishDate"]);
                    values.Add("IdMusicLabel", (string)reader["IdMusicLabel"]);
                    album = AlbumFromValues(values);

                }

            }
            finally
            {
                this.connection.Close();
            }

            return album;
        }

        public async Task<IEnumerable<Track>> GetTracks(int idAlbum)
        {
            List<Track> tracks = new List<Track>();
            Album album = null;
            try
            {
                this.connection.Open();
                SqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Track WHERE IdAlbum=@id";

                cmd.Parameters.Add(new SqlParameter("@id", idAlbum));

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    NameValueCollection values = new NameValueCollection();
                    values.Add("IdTrack", (string)reader["IdTrack"]);
                    values.Add("TrackName", (string)reader["TrackName"]);
                    values.Add("Duration", (string)reader["Duration"]);
                    values.Add("IdMusicAlbum", (string)reader["IdMusicAlbum"]);
                    tracks.Add(TrackFromValues(values));
                }
            }
            finally
            {
                this.connection.Close();
            }
            return tracks;
        }

        public async Task<bool> MusicianExists(int id)
        {
            Musician album = null;
            try
            {
                this.connection.Open();
                SqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Musician WHERE IdMusician=@id";

                cmd.Parameters.Add(new SqlParameter("@id", id));

                int counter = (int)cmd.ExecuteScalar();
                if (counter > 0)
                {
                    return true;
                }
            }
            finally
            {
                this.connection.Close();
            }

            return false;
        }

        public async Task<bool> DeleteMusician(int id)
        {
            bool result = false;
            try
            {
                this.connection.Open();
                SqlCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "DELETE FROM Musician WHERE IdMusitian = @id";
                cmd.Parameters.Add(new SqlParameter("@id", id));

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows == 1)
                {
                    result = true;
                }
            }
            finally
            {
                this.connection.Close();
            }

            return result;
        }

        private static Track TrackFromValues(NameValueCollection values)
        {
            Track track = new Track();
            track.IdTrack = Int32.Parse(values["IdTrack"]);
            track.TrackName = values["TrackName"];
            track.Duration = float.Parse("Duration");
            track.IdMusicAlbum = Int32.Parse(values["IdMusicAlbum"]);
            return track;
        }


        private static Album AlbumFromValues(NameValueCollection values)
        {
            Album album = new Album();
            album.IdAlbum = Int32.Parse(values["IdAlbum"]);
            album.AlbumName = values["AlbumName"];
            album.PublishDate = DateTime.ParseExact(values["PublishDate"], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            album.IdMusicLabel = Int32.Parse(values["IdMusicLabel"]);
            return album;
        }
    }
}
