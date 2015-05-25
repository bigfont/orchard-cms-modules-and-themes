$(function () {

    $('.blog-post').find('h3,h4,h5').each(function () {
        $this = $(this);
        $this.add($this.next('p')).wrapAll('<div class="run-in" />');
    })

});



