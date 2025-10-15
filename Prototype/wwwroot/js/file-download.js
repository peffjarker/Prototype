// Basic JS helper to download a base64 PDF
window.files = {
    save: (fileName, base64Data) => {
        try {
            const link = document.createElement("a");
            link.href = "data:application/pdf;base64," + base64Data;
            link.download = fileName;
            link.target = "_self";
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        } catch (e) {
            console.error("Error saving file:", e);
        }
    }
};
