using System.Threading.Tasks;
using BookStore.Localization;
using BookStore.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace BookStore.Web.Menus
{
    public class BookStoreMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
                await ConfigureBookStoreMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<BookStoreResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    BookStoreMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fas fa-home",
                    order: 0
                )
            );

            if (MultiTenancyConsts.IsEnabled)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

            

            return Task.CompletedTask;
        }

        private Task ConfigureBookStoreMenuAsync(MenuConfigurationContext context)
        {
            //add books page to the main menu
            var bookStoreMenu = new ApplicationMenuItem(
                "BookStore",
                "Book Store",
                icon: "fa fa-book"
            );

            context.Menu.AddItem(bookStoreMenu);

            bookStoreMenu.AddItem(
                new ApplicationMenuItem(
                    "BookStore.Books",
                    "Books",
                    url: "/Books"
                )
            );

            bookStoreMenu.AddItem(
                new ApplicationMenuItem(
                    "BookStore.Authors",
                    "Authors",
                    url: "/Authors"
                )
            );

            bookStoreMenu.AddItem(
                new ApplicationMenuItem(
                    "BookStore.Categories",
                    "Categories",
                    "/Categories"
                )
            );

            return Task.CompletedTask;
        }
    }
}