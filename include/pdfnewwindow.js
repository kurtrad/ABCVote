
Pdf=function(){this.pdfbuilt=0;this.RunPDF=function(){var pdiv=document.getElementById("progress"),el=document.createElement("iframe"),vars={},paramsStr=window.location.href;if(paramsStr.substr(paramsStr.length-1)=='#'){paramsStr=paramsStr.substr(0,paramsStr.length-1);}
paramsStr.replace(/[?&]+([^=&]+)=([^&]*)/gi,function(m,key,value){vars[key]=value;});document.body.appendChild(el);$(el).css('position','absolute');$(el).css('left','-1000px');var url=decodeURIComponent(vars["url"]);el.src=url+(url.indexOf('?')>=0?'&':'?')
+'pdf=1&width='+vars['width']+'&height='+vars['height']+'&rndval='+Math.random();if($('.progress_bar').length==0)
this.SetContent("<p>"+Runner.lang.constants.TEXT_PDF_BUILD1
+"<br><span style=\"display:block;background:white;border:solid black 1px;width:100px;height:20px;\"><span class=progress_bar style=\"display:block;background:#6080FF;width:1px;height:100%\"></span></span><br><span class=progress_percent></span>% "
+Runner.lang.constants.TEXT_PDF_BUILD2+"</p>");this.pdfbuilt=0;};this.SetProgress=function(total,progress){if(isNaN(total)||isNaN(progress))
return;var count=Math.floor(progress*100/total);if(count>100)
count=100;$('.progress_bar').css('width',""+count+"px");$('.progress_percent').html(""+count);}
this.SetContent=function(content){if($('[class*="printpdf"]').length)
$('[class*="printpdf"]').html(content);if($('[class*="viewpdf"]').length)
$('[class*="viewpdf"]').html(content);}};Runner.Pdf=new Pdf();Runner.Pdf.RunPDF();