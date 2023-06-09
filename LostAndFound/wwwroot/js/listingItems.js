﻿function ModalItemLostMatch() {
    var self = this;

    self.Items = ko.observableArray();
    self.LostItemId = ko.observable();

    self.Initialize = function (items) {
        var itemsListVM = [];
        for (var i = 0; i < items.length; i++) {
            var itemVM = new ItemListingViewModel(self);
            itemVM.Initialize(items[i]);
            itemsListVM.push(itemVM);
        }
        self.Items(itemsListVM);
    }

    self.Close = function () {
        $("#itemLostMatchModal").css("display", "none");
    }
}

function ModalDetails() {
    var self = this;

    self.Category = ko.observable();
    self.Color = ko.observable();
    self.DateAndTime = ko.observable();
    self.County = ko.observable();
    self.City = ko.observable();
    self.Address = ko.observable();
    self.ContactEmail = ko.observable();
    self.ContactNumber = ko.observable();
    self.Description = ko.observable();
    self.Reward = ko.observable();
    self.Cost = ko.observable();
    self.PictureContent = ko.observable();

    self.Initialize = function (data) {
        self.Category(data.Category);
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
        self.PictureContent(data.PictureContent);
    }

    self.Close = function () {
        $("#myModal").css("display", "none");
    }
}

function ItemListingViewModel(parent) {
    var self = this;

    self.Parent = parent;

    self.Id = ko.observable();
    self.Category = ko.observable();
    self.Color = ko.observable();
    self.DateAndTime = ko.observable();
    self.County = ko.observable();
    self.City = ko.observable();
    self.Address = ko.observable();
    self.ContactEmail = ko.observable();
    self.ContactNumber = ko.observable();
    self.Description = ko.observable();
    self.Reward = ko.observable();
    self.Cost = ko.observable();
    self.PictureContent = ko.observable();

    self.Initialize = function (data) {
        self.Id(data.Id);
        self.Category(data.ItemType);
        self.Color(data.Color);
        if (data.DateAndTime) {
            self.DateAndTime(data.DateAndTime);
        }
        self.County(data.County);
        self.City(data.City);
        self.Address(data.Address);
        self.ContactEmail(data.ContactEmail);
        self.ContactNumber(data.ContactNumber);
        self.Description(data.Description);
        self.Reward(data.Reward);
        self.Cost(data.Cost);
        self.PictureContent(data.PictureContent);
    }

    self.ShowModal = function () {

        var modalVM = new ModalDetails();

        modalVM.Initialize({
            "Category": self.Category(),
            "Color": self.Color(),
            "DateAndTime": self.DateAndTime(),
            "County": self.County(),
            "City": self.City(),
            "Address": self.Address(),
            "ContactEmail": self.ContactEmail(),
            "ContactNumber": self.ContactNumber(),
            "Description": self.Description(),
            "Reward": self.Reward(),
            "Cost": self.Cost(),
            "PictureContent": self.PictureContent()
        })

        ko.cleanNode(document.getElementById('myModal'));
        ko.applyBindings(modalVM, document.getElementById('myModal'));

        $("#myModal").css("display", "block");
    }

    self.SaveMatch = function () {
        var lostItemId = self.Parent.LostItemId();
        var foundItemId = self.Id();

        var postModel = {
            "FoundItemId": foundItemId,
            "LostItemId": lostItemId
        }

        var url = "/CreateMatch";
        ajaxHelper.post(url, postModel,
            function (result) {
                toastr.success('Successfully!');
                setTimeout(function () {
                    window.location.href = "/";
                }, 1000);
            },
            function (err) {
                toastr.error('Error!');
            });
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
        self.InitializeMatchItems();
    }

    self.InitializeLostItems = function () {
        var url = "/GetLostItems";
        ajaxHelper.getWithoutData(url,
            function (result) {
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
        var url = "/GetFoundItems";
        ajaxHelper.getWithoutData(url,
            function (result) {
                var items = result.Data;

                var itemsListVM = [];
                for (var i = 0; i < items.length; i++) {
                    var itemVM = new ItemListingViewModel();
                    itemVM.Initialize(items[i]);
                    itemsListVM.push(itemVM);
                }

                self.FoundItems(itemsListVM);
            },
            function (err) {
            });
    }
    self.InitializeMatchItems = function () {
        var url = "/GetMatchItems";
        ajaxHelper.getWithoutData(url,
            function (result) {
                var items = result.Data;

                var itemsListVM = [];
                for (var i = 0; i < items.length; i++) {
                    var itemVM = new ItemListingViewModel();
                    itemVM.Initialize(items[i]);
                    itemsListVM.push(itemVM);
                }

                self.MatchItems(itemsListVM);
            },
            function (err) {
            });
    }
}