import $, { get } from 'jquery';
import 'bootstrap';
import 'bootstrap-select';


const filt = {
    cities: [],
    venues: [],
    page: 1,
    pageSize: 3,
    totalPages: 0,
    sortBy: 'Date',
    sortOrder: 'Ascending'
};

const selects = {
    cities: [],
    venues: []
};

function createItem(item) {
    return `<div class="block1ForEvent">
                    <div class="card text-md-center">
                        <h3 align="center">${item.name}</h3>
                        <div class="card-img">
                            <img src="/img/${item.banner}" width="165" height="245" vspace="50" hspace="30" class="leftimg" />${item.description}
                            <div class="card-body">
                                <div><h6 class="h66">${getDate(item.date)} at ${getTime(item.date)}</h6></div>
                                <a href="/Event/Buy/${item.id}" class="btn btn-primary btnforEvList"><span>Buy Ticket</span></a>                              
                            </div>
                        </div>
                   </div>                 
                ${roleValidation(item)}`;
}

function createSelectItem(item) {
    return `<option value="${item.id}">${item.name}</option>`;
}

function getDate(date) {
    let dateFormat = require("dateformat");
    let dateform = new Date(date);
    return dateFormat(dateform, "dddd mmmm dS, yyyy");
}

function getTime(date) {
    let dateFormat = require("dateformat");
    let dateform = new Date(date);
    return dateFormat(dateform, "HH:MM");
}

function roleValidation(item) {
    if (role == "Administrator") {
        let stringforadmin = `<a href="/Event/Edit/${item.id}" class="btnft btn btnsg2 btn-primary"><span>Edit</span></a>
                <a href="/Event/Delete/${item.id}" class="btn btn-danger btnsg2 compItem"><span>Delete</span></a> 
                </div>`;

        return stringforadmin;
    } else {
        let stringforothers = `</div>`;
        return stringforothers;
    }
}

$(document).ready(function getCities() {
    $.ajax({
        url: `/api/v1/city`,
        data: filt.cities,
        traditional: true,
        success: function (data) {
            selects.cities = data;
            $("#cities").empty().append($.map(data, createSelectItem));
        }
    });
    
});

$(document).ready(function () {
    GetPageData(1, 3);
    $("#cities").on('change', function () {
        filt.cities = $(this).val();
        selects.cities = filt.cities;      
        getVenues();
        GetPageData(filt.page, filt.pageSize);
    });
    $("#venues").on('change', function () {
        filt.venues = $(this).val();
        GetPageData(filt.page, filt.pageSize);
    });
});

function getVenues() {
    $.ajax({
        url: `/api/v1/venue/getvenues`,
        data: selects,
        traditional: true,
        success: function (data) {
            selects.venues = data;
            $("#venues").empty().append($.map(data, createSelectItem));
            $("#venues").prop("disabled", false);
        }
    });
}

function GetPageData(pageNum, pageSize) {
    filt.page = pageNum;
    filt.pageSize = pageSize;
    $("#tblData").empty();
    $("#paged").empty();
    $.ajax({
        url: `/api/v1/pagination/pagedata`,
        data: filt,
        traditional: true,
        success: function (data, status, xhr) {
            $("#items").empty().append($.map(data, createItem));
            const count = xhr.getResponseHeader('x-total-count');
            const current = xhr.getResponseHeader('x-current-page');
            console.log(count);
            console.log(current);
            PaggingTemplate(count, current);
        }
    });
}

function PaggingTemplate(totalPage, currentPage) {
    let template = "";
    filt.totalPages = totalPage;
    let TotalPages = totalPage;
    let CurrentPage = currentPage;
    let PageNumberArray = [];

    for (let i = 1; i <= totalPage; i++) {      
        PageNumberArray[i] = i;
    };

    console.log(PageNumberArray);
    let FirstPage = 1;
    let LastPage = totalPage;
    if (TotalPages != CurrentPage) {
        var ForwardOne = CurrentPage + 1;
    }
    let BackwardOne = 1;
    if (CurrentPage > 1) {
        BackwardOne = CurrentPage - 1;
    };

    template = `<p>${filt.page} of ${filt.totalPages} pages</p>`;
    template = template + `<ul class="pagination">` +
        `<li><select ng-model="pageSize" id="selectedId"><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="5">5</option></select> </li>` +
        `<li><a href="#" onclick="GetPageData(${FirstPage, TotalPages})" class="page-link"><i class=""></i>First</a></li>` +       
        `<li><a href="#" onclick="GetPageData(${BackwardOne, TotalPages})"><i class="page-link">&#8592;</i></a></li>`;

    let numberingLoop = "";
    for (let i = 1; i < PageNumberArray.length; i++) {
        numberingLoop = numberingLoop + `<li><a class="page-link" onclick="GetPageData(${PageNumberArray[i], TotalPages})" href="#">${PageNumberArray[i]}</a></li>`
    };
    template = template + numberingLoop + `<li><a href="#" class="page-link" onclick="GetPageData(${ForwardOne, TotalPages})"><i>&#8594;</i></a></li>` +
        `<li><a href="#" class="page-link" onclick="GetPageData(${LastPage, TotalPages})">Last<i></i></a></li></ul>`;

    $("#paged").append(template);
    $('#selectedId').change(function () {
        GetPageData(1, $(this).val());
    });

}
