const api = {
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
};

export default api;
