window.colorTools = {
    extractColors: async function (imgSelector) {
        console.log("colorTools.extractColors running for:", imgSelector);

        return new Promise((resolve) => {

            // slight delay to ensure DOM + Blazor finished rendering
            setTimeout(() => {
                const img = document.querySelector(imgSelector);
                console.log("Image found?", img);

                if (!img) {
                    console.log("NO IMAGE FOUND, returning null");
                    resolve(null);
                    return;
                }

                // If naturalWidth === 0 → image isn't ready
                if (img.naturalWidth === 0) {
                    console.log("Image not ready yet. Returning null.");
                    resolve(null);
                    return;
                }

                console.log("Extracting from image with size:", img.naturalWidth, img.naturalHeight);

                try {
                    const canvas = document.createElement("canvas");
                    canvas.width = img.naturalWidth;
                    canvas.height = img.naturalHeight;
                    const ctx = canvas.getContext("2d");
                    ctx.drawImage(img, 0, 0);

                    const data = ctx.getImageData(0, 0, canvas.width, canvas.height).data;
                    console.log("Pixel data length:", data.length);

                    let r = 0, g = 0, b = 0;
                    const step = 10;

                    for (let i = 0; i < data.length; i += 4 * step) {
                        r += data[i];
                        g += data[i + 1];
                        b += data[i + 2];
                    }

                    const count = data.length / (4 * step);
                    const palette = {
                        primary: `rgb(${Math.round(r / count)}, ${Math.round(g / count)}, ${Math.round(b / count)})`,
                        primaryDark: `rgb(${Math.max(0, Math.round(r / count) - 40)}, ${Math.max(0, Math.round(g / count) - 40)}, ${Math.max(0, Math.round(b / count) - 40)})`,
                        primaryLight: `rgb(${Math.min(255, Math.round(r / count) + 40)}, ${Math.min(255, Math.round(g / count) + 40)}, ${Math.min(255, Math.round(b / count) + 40)})`
                    };

                    console.log("Palette extracted:", palette);
                    resolve(palette);
                } catch (err) {
                    console.error("EXTRACTION FAILED:", err);
                    resolve(null);
                }
            }, 200); // 200ms wait
        });
    }
};
