window.functions = {
    saveAsExcelFile(fileName, byteBase64) {
        this.saveAsFile(fileName, byteBase64, 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,');
    },
    saveAsWordFile(fileName, byteBase64) {
        this.saveAsFile(fileName, byteBase64, 'data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,');
    },
    saveAsPdfFile(fileName, byteBase64) {
        this.saveAsFile(fileName, byteBase64, 'data:application/pdf;base64,');
    },
    saveAsFile(fileName, byteBase64, contentType) {
        var link = document.createElement('a');
        link.download = fileName;
        link.href = contentType + byteBase64;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }
};