using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSoundcloudApi.Models;

namespace TestSoundcloudApi
{
    public interface ISearch
    {
        public Task<Root> SearchAsync(string track, uint limit = 2);
    }
}
