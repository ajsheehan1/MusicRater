﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class SongRatingCreate
    {
        public int SongId { get; set; }
        public decimal SongIndividualRating { get; set; }
    }
}