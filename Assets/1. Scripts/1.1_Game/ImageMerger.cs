using UnityEngine;

public class ImageMerger : MonoBehaviour
{
    public static Texture2D InsertTextureBottom(Texture2D background, Texture2D overlay)
    {
        Texture2D combinedTexture = new Texture2D(background.width, background.height);
        combinedTexture.SetPixels(background.GetPixels());

        for (int x = 0; x < overlay.width; x++)
        {
            for (int y = 0; y < overlay.height; y++)
            {
                int targetX = x;
                int targetY = y;

                if (targetX < combinedTexture.width && targetY < combinedTexture.height)
                {
                    Color colorA = overlay.GetPixel(x, y);
                    // Color colorB = background.GetPixel(targetX, targetY);

                    // Trộn màu từ texture A và B dựa trên alpha của texture A
                    Color combinedColor = colorA;
                    // Color combinedColor = Color.Lerp(colorB, colorA, colorA.a);
                    combinedTexture.SetPixel(targetX, targetY, combinedColor);
                }
            }
        }

        combinedTexture.Apply();
        return combinedTexture;
    }

    public static Texture2D InsertTextureRight(Texture2D background, Texture2D overlay)
    {
        Texture2D combinedTexture = new Texture2D(background.width, background.height);
        combinedTexture.SetPixels(background.GetPixels());

        for (int x = 0; x < overlay.width; x++)
        {
            for (int y = 0; y < overlay.height; y++)
            {
                int targetX = background.width - y;
                int targetY = x;

                if (targetX < combinedTexture.width && targetY < combinedTexture.height)
                {
                    Color colorA = overlay.GetPixel(x, y);
                    Color colorB = combinedTexture.GetPixel(targetX, targetY);

                    // Trộn màu từ texture A và B dựa trên alpha của texture A
                    Color combinedColor = colorA;
                    // Color combinedColor = Color.Lerp(colorB, colorA, colorA.a);
                    combinedTexture.SetPixel(targetX, targetY, combinedColor);
                }
            }
        }

        combinedTexture.Apply();

        return combinedTexture;
    }

    public static Texture2D InsertTextureLeft(Texture2D background, Texture2D overlay)
    {
        Texture2D combinedTexture = new Texture2D(background.width, background.height);
        combinedTexture.SetPixels(background.GetPixels());

        for (int x = 0; x < overlay.width; x++)
        {
            for (int y = 0; y < overlay.height; y++)
            {
                int targetX = y;
                int targetY = x;

                if (targetX < combinedTexture.width && targetY < combinedTexture.height)
                {
                    Color colorA = overlay.GetPixel(x, y);
                    Color colorB = combinedTexture.GetPixel(targetX, targetY);

                    // Trộn màu từ texture A và B dựa trên alpha của texture A
                    Color combinedColor = colorA;
                    // Color combinedColor = Color.Lerp(colorB, colorA, colorA.a);
                    combinedTexture.SetPixel(targetX, targetY, combinedColor);
                }
            }
        }

        combinedTexture.Apply();

        return combinedTexture;
    }

    public static Texture2D InsertTextureTop(Texture2D background, Texture2D overlay)
    {
        Texture2D combinedTexture = new Texture2D(background.width, background.height);
        combinedTexture.SetPixels(background.GetPixels());

        for (int x = 0; x < overlay.width; x++)
        {
            for (int y = 0; y < overlay.height; y++)
            {
                int targetX = background.width - x;
                int targetY = background.height - y;

                if (targetX < combinedTexture.width && targetY < combinedTexture.height)
                {
                    Color colorA = overlay.GetPixel(x, y);
                    Color colorB = combinedTexture.GetPixel(targetX, targetY);

                    // Trộn màu từ texture A và B dựa trên alpha của texture A
                    Color combinedColor = colorA;
                    // Color combinedColor = Color.Lerp(colorB, colorA, colorA.a);
                    combinedTexture.SetPixel(targetX, targetY, combinedColor);
                }
            }
        }

        combinedTexture.Apply();

        return combinedTexture;
    }
}
