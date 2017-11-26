$(document).ready(function () {
  var $taginput = $('#tags-search-input');

  $taginput.change(function () {
    var self = $(this);
    var tags = self.val();
    console.log('Tags changed: ' + tags);

    var $newsContainer = $('#news-container');
    var category = $newsContainer.data('category');
    console.log('Category: ' + category);

    $.ajax({
      url: 'News/IndexPartial',
      method: 'GET',
      data: { category: category, tags: tags }
    })
      .done(function (data) {
        var $target = $('#news-container');
        $target.html(data);
      })
      .fail(function () {
        console.warn("Fail");
      });
  });
});