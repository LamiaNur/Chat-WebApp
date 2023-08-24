using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Framework.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Migration.Job
{
    public interface IMigrator
    {
        Task MigrateAsync();
    }

    [ServiceRegister(typeof(IMigrator), ServiceLifetime.Transient)]
    public class Migrator : IMigrator
    {
        private int _counter = 0;
        public async Task MigrateAsync()
        {
            await Task.Delay(5000);
            Console.WriteLine($"On Migrate async count : {_counter}");
            await Task.CompletedTask;
            _counter++;
        } 
    }
}
