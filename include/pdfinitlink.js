
$('.pdflink').attr("target","_blank").click(function(){var maxwidth=0,maxheight=0,minleft=999999,mintop=999999,isOldLayout=false,containerClass='rnr-s-',emptyClass='rnr-s-empty',containers;containers=$('[name="page"]').each(function(ind,element){if($('table[class*="runner-c-grid"]',element).length>0)
element=$('table[class*="runner-c-grid"]',element).eq(0)[0];if(element.offsetWidth>maxwidth)
maxwidth=element.offsetWidth;if(element.offsetHeight>maxheight)
maxheight=element.offsetHeight;});if(!containers.length){isOldLayout=!!$('div[class^="runner-s-"]').length;if(isOldLayout){containerClass='runner-s-';emptyClass='runner-s-empty';}
containers=$('div[class*="'+containerClass+'"]').not('.'+emptyClass).each(function(){var jQElement=$(this),firstChild=jQElement.children(':eq(0)');if(firstChild[0].offsetLeft+firstChild[0].offsetWidth>maxwidth){maxwidth=firstChild[0].offsetLeft+firstChild[0].offsetWidth;}
if(firstChild[0].offsetTop+firstChild[0].offsetHeight>maxheight){maxheight=firstChild[0].offsetTop+firstChild[0].offsetHeight;}
if(firstChild[0].offsetLeft<minleft){minleft=firstChild[0].offsetLeft;}
if(firstChild[0].offsetTop<mintop){mintop=firstChild[0].offsetTop;}});}
maxwidth=maxwidth||document.body.scrollWidth;maxheight=maxheight||document.body.scrollHeight;if(mintop<999999){maxheight-=mintop;}
if(minleft<999999){maxwidth-=minleft;}
var href=window.location.href;var ind=href.indexOf("?");var url=Runner.getPageUrl("buildpdf")+"?url=";if(ind!=-1){url+=encodeURIComponent(href.substring(0,ind)+"?pdf=1&"+href.substring(ind+1,href.length));url+="&"+href.substring(ind+1,href.length);}
else{url+=encodeURIComponent(href+"?pdf=1");}
$(this).attr("href",url+'&rndval='+Math.random());});