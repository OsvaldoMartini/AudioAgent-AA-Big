
function ViewModel() {
    var self = this;
    self.rangeSelect = ko.observable('all');

    self.showRow = ko.observable(false);

    self.imageUrl = ko.observable("");

    self.companyImagesUrlList = ko.observableArray();

    self.toggleVisibility = function () {
        self.showRow(!self.showRow());
    };

    self.dataGetter = ko.computed(function () {
        var range = self.rangeSelect();

        if (self.imageUrl() != '') {

            range = self.imageUrl(); //'https://www.countries-ofthe-world.com/flags-normal/flag-of-Australia.png';

            self.imageUrl("");

            if (range.includes("https://")) {
                range = range.replace("https://", "https");
            }
            else if (range.includes("https://")) {
                range = range.replace("http://", "http");
            }

            var res = fixedEncodeURIComponent(range);
            res = escapeURIparam(range);

            //var preUrl = 'http://wservices.co.uk/services/GeoService.svc/Backend/' + range;
            //var preUrl = 'http://localhost:59005/GeoService.svc/Backend/' + range;
            var preUrl = 'http://localhost:59005/ImageUrlSearch.svc/v1/Search/' + res;

            var data;

            var listImagesUrl = [];

            $.ajax({
                type: "GET",
                async: false,
                contentType: "application/json; charset=utf-8",
                url: preUrl,//'http:www.svc/Service1.svc/GetJson',
                //data: "{ }",
                processData: false,
                dataType: "jsonp",
                success: function (data) {
                    var result = data.SearchResult;

                    result.forEach(function (item, index, array) {
                        var imageObj = {};
                        imageObj['CompanyImageID'] = item.CompanyImageID;
                        imageObj['CompanyType'] = item.TCompany.CompanyType;
                        imageObj['ImageUrl'] = item.ImageUrl;
                        imageObj['ImageSizeMB'] = item.ImageSizeMB;
                        imageObj['ImageExtension'] = item.ImageExtension;
                        listImagesUrl.push(imageObj);
                    });

                    self.companyImagesUrlList(listImagesUrl);
                    if (listImagesUrl.length === 0)
                        self.showRow(false);
                    else
                        self.showRow(true);
                 },
                error: function (xhr) {
                    self.showRow(false);
                    alert('Service: ' + xhr.status + '-' + xhr.statusText + ':' + preUrl);
                }
            });
        } else {
            self.showRow(false);
        }
    });

}

var escapeURIparam = function (url) {
    if (encodeURIComponent) url = encodeURIComponent(url);
    else if (encodeURI) url = encodeURI(url);
    else url = escape(url);
    url = url.replace(/\+/g, '%2B'); // Force the replacement of "+"
    return url;
};

function fixedEncodeURIComponent(str) {
    return encodeURIComponent(str).replace(/[!'()*]/g, function (c) {
        return '%' + c.charCodeAt(0).toString(16);
    });
}

//Activate knockout.js
ko.applyBindings(new ViewModel());


ko.bindingHandlers.date = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor(), allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        var d = "";
        if (valueUnwrapped) {
            var m = /Date\([\d+-]+\)/gi.exec(valueUnwrapped);
            if (m) {
                d = String.format("{0:dd/MM/yyyy}", eval("new " + m[0]));
            }
        }
        $(element).text(d);
    }
};
ko.bindingHandlers.money = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor(), allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        var m = "";
        if (valueUnwrapped) {
            m = parseInt(valueUnwrapped);
            if (m) {
                m = String.format("{0:n0}", m);
            }
        }
        $(element).text(m);
    }
};
