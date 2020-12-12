import $, { get } from 'jquery';
import 'jquery';
import 'bootstrap-select';


const filters = {
    cities: [],
    venues: [],
    page: 1,
    pageSize: 5,
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
    var dateFormat = require("dateformat");
    var dateform = new Date(date);
    return dateFormat(dateform, "dddd mmmm dS, yyyy");
}

function getTime(date) {
    var dateFormat = require("dateformat");
    var dateform = new Date(date);
    return dateFormat(dateform, "HH:MM");
}

function roleValidation(item) {
    if (role == "Administrator") {
        var stringforadmin = `<a href="/Event/Edit/${item.id}" class="btnft btn btnsg2 btn-primary"><span>Edit</span></a>
                <a href="/Event/Delete/${item.id}" class="btn btn-danger btnsg2 compItem"><span>Delete</span></a> 
                </div>`;

        return stringforadmin;
    } else {
        var stringforothers = `</div>`;
        return stringforothers;
    }
}

$(document).ready(function getCities() {
    $.ajax({
        url: `/api/v1/city`,
        data: filters.cities,
        traditional: true,
        success: function (data) {
            selects.cities = data;
            $("#cities").empty().append($.map(data, createSelectItem));
            $('.selectpicker').selectpicker('refresh');
        }
    });
    
});

$(document).ready(function () {
    getEvents();   
    $("#cities").on('change', function () {
        filters.cities = $(this).val();
        selects.cities = filters.cities;      
        getVenues();
        getEvents();
    });
    $("#venues").on('change', function () {
        filters.venues = $(this).val();
        getEvents();
    });
    $('.selectpicker').selectpicker('refresh');
});

function getVenues() {
    $.ajax({
        url: `/api/v1/venue/getvenues`,
        data: selects,
        traditional: true,
        success: function (data) {
            selects.venues = data;
            $("#venues").empty().append($.map(data, createSelectItem));
            //$("#venues").("disabled", true);
        }
    });
}

function getEvents() {
    $.ajax({
        url: `/api/v1/events/getfiltred`,
        data: filters,
        traditional: true,
        success: function (data, status, xhr) {
            $("#items").empty().append($.map(data, createItem));
            const count = xhr.getResponseHeader('x-total-count');
            addPaginationButtons(filters.page, count, filters.pageSize);
        }
    });
}

function addPaginationButtons(currentPage, totalCount, pageSize) {
    const pageCount = Math.ceil(totalCount / pageSize);
    const buttons = [];
    for (let i = 1; i <= pageCount; i++) {
        const button = $('<li>', { class: 'page-item' });
        if (i === currentPage) {
            button.addClass('active');
            button.append(`<a class="page-link" href="#">${i} <span class="sr-only">(current)</span></a>`)
        } else {
            button.append(`<a class="page-link" href="#">${i}</a>`)
        }
        buttons.push(button);
    }
    $('.pagination').empty().append(buttons);
}
