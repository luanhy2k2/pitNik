﻿using Core.Entities;
using Core.Interface.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ReplyCommentRepository : GenericRepository<ReplyComment>, IReplyCommentRepository
    {
        public ReplyCommentRepository(PitNikDbContext context) : base(context)
        {
        }
    }
}
