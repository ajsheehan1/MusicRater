using MusicRater.Data;
using MusicRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Services
{
    public class StoreService
    {
        private readonly Guid _userId;

        public StoreService(Guid userId)
        {
            _userId = userId;
        }

        public bool Create(StoreCreate model)
        {
            var entity =
                new Store()
                {
                    OwnerId = _userId,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Stores.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<StoreListItem> GetStores()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Stores
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new StoreListItem
                                {
                                    StoreId = e.StoreId,
                                    StoreName = e.StoreName,
                                }
                        );

                return query.ToArray();
            }
        }
    }
}
