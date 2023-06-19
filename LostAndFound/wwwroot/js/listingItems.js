function ItemListingViewModel() {
    var self = this;

    self.Category = ko.observable();
    self.Color = ko.observable();
    self.DateAndTime = ko.observable(new Date());
    self.County = ko.observable();
    self.City = ko.observable();
    self.Address = ko.observable();
    self.ContactEmail = ko.observable();
    self.ContactNumber = ko.observable();
    self.Description = ko.observable();
    self.Reward = ko.observable();
    self.Cost = ko.observable();

    self.Initialize = function (data) {
        self.Category(data.ItemType);
        self.Color(data.Color);
        self.DateAndTime(data.DateAndTime);
        self.County(data.County);
        self.City(data.City);
        self.Address(data.Address);
        self.ContactEmail(data.ContactEmail);
        self.ContactNumber(data.ContactNumber);
        self.Description(data.Description);
        self.Reward(data.Reward);
        self.Cost(data.Cost);
    }
} 

function ListingPageViewModel() {
    var self = this;

    self.LostItems = ko.observableArray([]);
    self.FoundItems = ko.observableArray([]);
    self.MatchItems = ko.observableArray([]);

    self.InitializePage = function () {
        self.InitializeLostItems();
        self.InitializeFoundItems();
        //self.InitializeMatchItems();
    }

    self.InitializeLostItems = function () {
        debugger
        var url = "/GetLostItems";
        ajaxHelper.getWithoutData(url,
            function (result) {
                debugger
                var items = result.Data;

                var itemsListVM = [];
                for (var i = 0; i < items.length; i++) {
                    var itemVM = new ItemListingViewModel();
                    itemVM.Initialize(items[i]);
                    itemsListVM.push(itemVM);
                }

                self.LostItems(itemsListVM);
            },
            function (err) {
            });
    }
    self.InitializeFoundItems = function () {
        debugger
        var url = "/GetFoundItems";
        ajaxHelper.getWithoutData(url,
            function (result) {
                debugger
            },
            function (err) {
            });
    }
    self.InitializeMatchItems = function () {
        var url = "/GetLostItems";
        ajaxHelper.getWithoutData(url,
            function (result) {
                debugger
            },
            function (err) {
            });
    }
}