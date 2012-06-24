
$(function ()
{
    $('#myfileupload').MultiFile({
        max: 5,
        accept: 'gif|jpg|png|bmp',
        STRING: {
            remove: "[删除]",
            selected: 'Selecionado: $file',
            denied: '不支持上传 $ext 格式的文件!',
            duplicate: '文件已经在上传列表中: $file'
        },
        list: '#fileList'
    });
});