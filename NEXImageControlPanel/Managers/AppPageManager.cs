using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public static Expression<Func<T>> CreateNewInstanceFunc<T>(T type)
        {
            NewExpression exp = Expression.New(type.GetType());

            // Define the type T
            return Expression.Lambda<Func<T>>(exp);
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
