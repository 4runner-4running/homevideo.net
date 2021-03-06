﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Domain.Contracts
{
    /// <summary>
    /// Defines a data input object for the LiteDB collection
    /// </summary>
    public interface IMovieData
    {
        Guid Id { get; set; }
        int MovieDbId { get; set; }
        DateTime ReleaseDate { get; set; }
        string DisplayName { get; set; }
        string FileName { get; set; }
        string Path { get; set; }
        long ContentLength { get; set; }
        bool Played { get; set; }
        DateTime LastPlayed { get; set; }
        DateTime DateAdded { get; set; }
        string MetadataDescription { get; set; }
        string PosterPath { get; set; }
        string BackdropPath { get; set; }
        byte[] ImageBytes { get; set; }
        byte[] BackdropBytes { get; set; }
        byte[] ThumbBytes { get; set; }
    }
}
