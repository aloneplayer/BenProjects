
$(function ()
{
    $("#myfileupload").uploadify({
        height: 30,
        swf: '/uploadify/uploadify.swf',
        uploader: '/home/MultiFileUploadWithUploadify',
        width: 120,
        auto:false,
    }); 
});