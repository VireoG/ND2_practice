import $ from 'jquery';

const filters = {
    cities: [],
    venues: [],
    page: 1,
    pageSize: 3,
    sortOrder: 'Ascending'
};


function createItem(item) {
    return `<div class="block1ForEvent">
                <div class="card text-md-center">
                <h3 align="center">${item.Name}</h3>
                <div class="card-img">
                    <img src="~/img/${item.Banner}" width="165" height="245" vspace="50" hspace="30" class="leftimg" />
        </div >`;
}

$(document).ready(function () {
    getEvents();
    $("#cities").on('change', function () {
        filters.cities = $(this).val();
        getEvents();
    });
    $("#venues").on('change', function () {
        filters.venues = $(this).val();
        getEvents();
    });
});

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

    // <li class="page-item"><a class="page-link" href="#">1</a></li>
    // <li class="page-item active" aria-current="page">
    //     <a class="page-link" href="#">2 <span class="sr-only">(current)</span></a>
    // </li>
    // <li class="page-item"><a class="page-link" href="#">3</a></li>
}