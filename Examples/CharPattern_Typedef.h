//Need this declaration before declaring your own table.
typedef struct CharPattern {
    unsigned char line0;
    unsigned char line1;
    unsigned char line2;
    unsigned char line3;
    unsigned char line4;
    unsigned char line5;
    unsigned char line6;
} CharPattern;
//If you need a complete 8x8 square you just need to add one more line title it line7 (or whatever you want).
//This current configuration is for when the pixels are displayed horizontally, that is to say byte 1 is the first 8 pixels, byte 2 is the pixels 9-16, and so on.  These pixels are horizontally and not vertically.
