using HomeVideo.Net.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.DataObjects
{
    public class MovieData : IMovieData, IMedia
    {
        public Guid Id { get; set; }
        public int MovieDbId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string DisplayName { get; set; }
        public string FileName {get;set;}
        public string Extension { get; set; }
        public string Path {get;set;}
        public long ContentLength {get;set;}
        public bool Played {get;set;}
        public DateTime LastPlayed {get;set;}
        public DateTime DateAdded {get;set;}
        public string MetadataDescription {get;set;}
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public byte[] ImageBytes { get; set; }
        public byte[] BackdropBytes { get; set; }
        public byte[] ThumbBytes { get; set; }
    }
}
