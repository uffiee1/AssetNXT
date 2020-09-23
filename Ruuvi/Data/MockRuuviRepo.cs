using System.Collections.Generic;
using Ruuvi.Models;

namespace Ruuvi.Data
{
    public class MockRuuviRepo : IRuuviRepo
    {
        public void CreateTag(Tag tag)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Tag> GetAllTags()
        {
            var tagItems = new List<Tag>
            {
                new Tag{Id=0, HowTo="ss33", Line="ss3", Platform="Kettle3"},
                new Tag{Id=1, HowTo="ss22", Line="ss2", Platform="Kettle2"},
                new Tag{Id=2, HowTo="ss11", Line="ss1", Platform="Kettle1"}
            };

            return tagItems;
        }

        public Tag GetTagById(int id)
        {
            return new Tag{Id=0, HowTo="ss", Line="ss", Platform="Kettle"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}