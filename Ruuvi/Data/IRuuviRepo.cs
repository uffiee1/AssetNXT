using System.Collections.Generic;
using Ruuvi.Models;

namespace  Ruuvi.Data
{
    // Here we are going to define the CRUD operations
    public interface IRuuviRepo
    {
        bool SaveChanges();

        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(int id);
        void CreateTag(Tag tag);
        void CreateOrUpdateTag(Tag tag);
    }
}