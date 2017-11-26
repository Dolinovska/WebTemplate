$(document).ready(function () {
  var $form = $('#create-news-form');
  var $title = $('#Title');

  $form.submit(function () {
    var self = $(this);
    console.log('Submit');

    var title = 'Новину додано!';
    var notification = new Notification(title);
});
});

