using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NEXImageControlPanel.Interfaces;
using NEXImageControlPanel.Models;

namespace NEXImageControlPanel.Managers
{
    public class AppPageManager : IAppPageManager
    {
        public static Func<T> CreateNewInstanceFunc<T>(T type) where T : new()
        {
            // Check if T has a parameterless constructor
            var ctor = typeof(T).GetConstructor(Type.EmptyTypes);
            return ctor == null
                ? throw new InvalidOperationException("No parameterless constructor found for this type.")
                :
                // Create and return a function that invokes the constructor
                () => (T)ctor.Invoke(null);
        }

        public List<MainWindowPage> GetAppPages()
        {
            // Get the current assembly
            var currentAssembly = Assembly.GetExecutingAssembly();

            // Find all types in the assembly that are subclasses of Page
            var pageTypes = currentAssembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Page)) && !t.IsAbstract);

            // For each type, if it has a parameterless constructor, create an instance
            var pageList = (from type in pageTypes let ctor = type.GetConstructor(Type.EmptyTypes) where ctor != null select (Page?)Activator.CreateInstance(type) into pageInstance where pageInstance != null select pageInstance).ToList();

            return pageList.Select(x => new MainWindowPage()
            {
                Title = x.Title,
                WindowPage = CreateNewInstanceFunc(x),
                PageName = x.GetType().Name
            }).ToList();
        }
    }
}
