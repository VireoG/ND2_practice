export const api = {
    getCities: function (filters) {
        return $.ajax({
            url: `/api/v1/city`,
            data: filters,
            traditional: true,
            success: function (data) {
                return data;
            },
        });
    },
    getVenues: function (selects) {
        return $.ajax({
            url: `/api/v1/venue/getvenues`,
            data: selects,
            traditional: true,
            success: function (data) {
                return data;
            },
        });
    },
};

export default api;