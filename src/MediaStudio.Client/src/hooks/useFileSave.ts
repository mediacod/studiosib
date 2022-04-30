import FileSaver from "file-saver";

export const useFileSave = () => {

        const trackSave = async ({link, name}: any) => {

            const xhr = new XMLHttpRequest()
            xhr.open("GET", link, true)
            xhr.responseType = "arraybuffer"

            xhr.onload = function(oEvent) {
                const blob = new Blob([xhr.response], {type: "audio/mp3"})
                FileSaver.saveAs(blob, `${name}.mp3`);
            }

            xhr.send()
        }

    return {trackSave}
};