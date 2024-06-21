namespace Extensionator.Tests {
    public class StringExtensionTests {

        [Fact]
        public void Should_Validate_URL() {
            Assert.True("http://www.example.com".IsValidUrl());
            Assert.True("https://www.google.com/search?q=test".IsValidUrl());
            Assert.True("http://localhost:8080".IsValidUrl());
            Assert.True("https://user:password@ftp.example.com".IsValidUrl());
            Assert.True("https://wikipedia.org/wiki/Main_Page#History".IsValidUrl());

            Assert.False("www.example.com".IsValidUrl());
            Assert.False("ftp://invalid-protocol.com".IsValidUrl());
            Assert.False("http://localhost:8080a".IsValidUrl());
            Assert.False("http:// www.example.com  ".IsValidUrl()); // Spaces
            Assert.False("http://".IsValidUrl());
            Assert.False("https://".IsValidUrl());
        }

        [Fact]
        public void Should_Strip_BBCode() {
            Assert.Equal("game development it's hard", "game [color=yellow]development[/color] it's hard".StripBBcode());

            var longerText = @"[quote][b]Here's an example of a complex quote with formatting![/b]

This text within the quote is normal. But now, let's [u]underline[/u] a part 
and make another part [color=green]green[/color].

[img]https://www.example.com/funny-cat.gif[/img]

See, this funny cat gif is also embedded within the quote![/quote]";

            Assert.Equal(@"Here's an example of a complex quote with formatting!

This text within the quote is normal. But now, let's underline a part 
and make another part green.

https://www.example.com/funny-cat.gif

See, this funny cat gif is also embedded within the quote!", longerText.StripBBcode());
        }

        [Fact]
        public void Should_Strip_HTML() {
            Assert.Equal("This text is bold.", "<b>This text is bold.</b>".StripHTML());
            Assert.Equal("This is a paragraph with some bold and italic text", "<p>This is a paragraph with some <b>bold</b> and <i>italic</i> text".StripHTML());
            Assert.Equal("This text is inside a div with a class", "<div class=\"my-class\">This text is inside a div with a class</div>".StripHTML());
            Assert.Equal("This is a heading!This is a paragraph with HTML structure", "<!DOCTYPE html><html><body><h1>This is a heading!</h1><p>This is a paragraph with HTML structure</p></body></html>".StripHTML());
        }

        [Fact]
        public void Should_Transform_To_Title_Case() {
            Assert.Equal("War And Peace", "wAr aNd pEaCe".ToTitleCase());
            Assert.Equal("Weinprobe Im Rheingau", "weinprobe im rheingau".ToTitleCase("de-DE"));
            Assert.Equal("10 Downing Street", "10 Downing Street".ToTitleCase());
            Assert.Equal("!@#$%^&*()", "!@#$%^&*()".ToTitleCase());
            Assert.Equal("", "".ToTitleCase());
            Assert.Equal("A", "a".ToTitleCase());
            Assert.Equal("Mother-In-Law", "mother-in-law".ToTitleCase());
        }

        [Fact]
        public void Should_Transform_To_Slug() {
            Assert.Equal("", "".ToSlug());
            Assert.Equal("hello_world", "Hello World".ToSlug());
            Assert.Equal("this_is_a_string", "This is a string".ToSlug());
            Assert.Equal("my_blog_post_title", "My blog post title".ToSlug());

            Assert.Equal("c_programming", "C# Programming".ToSlug());
            Assert.Equal("hola", "¡Hola!".ToSlug()); // Removes non-alphanumeric characters

            Assert.Equal("extra_spaces", "Extra   spaces".ToSlug());
            Assert.Equal("leading_space", "Leading  space".ToSlug());
            Assert.Equal("trailing_space", "Trailing  space  ".ToSlug());

            Assert.Equal("100_pure", "100% Pure".ToSlug()); // Preserves numbers
            Assert.Equal("all_lowercase", "all-lowercase".ToSlug());
            Assert.Equal("uppercase_text", "Uppercase Text".ToSlug()); // Converts to lowercase
        }

        [Fact]
        public void Should_Truncate_The_Provided_Text() {
            Assert.Equal("my super longer t...", "my super longer text that needs to be truncated".Truncate(20));
            Assert.Equal("my super longer text that---", "my super longer text that needs to be truncated".Truncate(28, "---"));
        }

        [Fact]
        public void Should_Compare_Ignoring_Case() {
            Assert.True("SAME".EqualsIgnoreCase("same"));
            Assert.True(".PNg".EqualsIgnoreCase(".png"));
            Assert.True("AAAAA".EqualsIgnoreCase("aaaaa"));
            Assert.True("bbbb".EqualsIgnoreCase("bbbb"));
            Assert.True("cccc".EqualsIgnoreCase("CCCC"));
        }

        [Fact]
        public void Should_Repeat_Selected_String() {
            Assert.Equal("*****", "*".Repeat(5));
            Assert.Equal("*****", '*'.Repeat(5)); // Should work with char values also

            Assert.Equal("eat my face eat my face ", "eat my face ".Repeat(2));
        }

        [Fact]
        public void Should_Detect_If_String_Is_Lowercase() {
            Assert.True("zeus".IsLower());
            Assert.False("Zeus".IsLower());
        }

        [Fact]
        public void Should_Detect_If_String_Is_Uppercase() {
            Assert.True("ZEUS".IsUpper());
            Assert.False("ZEUs".IsLower());
        }

        [Fact]
        public void Should_Remove_Invalid_Filename_Characters() {
            Assert.Equal("This is a valid filename.txt", "This is a valid filename.txt".RemoveInvalidFileNameCharacters());
            Assert.Equal("Testfile.doc", "Test*file.doc".RemoveInvalidFileNameCharacters());
            Assert.Equal("Special_Characters+.pdf", "Special_Characters<+>.pdf".RemoveInvalidFileNameCharacters());

            Assert.Equal("", "".RemoveInvalidFileNameCharacters());
            Assert.Equal(" ", " ".RemoveInvalidFileNameCharacters());

            Assert.Equal("Filename with.space", "Filename with.space".RemoveInvalidFileNameCharacters());
            Assert.Equal("100% Pure Data.xls", "100% Pure Data.xls".RemoveInvalidFileNameCharacters());
            Assert.Equal("にほんご.txt", "にほんご.txt".RemoveInvalidFileNameCharacters()); // Preserves Japanese characters
        }

        [Fact]
        public void Should_Remove_Invalid_Path_Characters() {
            Assert.Equal("C:\\This/is/a/valid/path.txt", "C:\\This/is/a/valid/path.txt".RemoveInvalidPathCharacters());
            Assert.Equal("Test*folder\\with\\illegal.doc", "Test*folder\\with\\illegal.doc".RemoveInvalidPathCharacters());

            Assert.Equal("C:\\Users\\John<Smith>.txt", "C:\\Users\\John<Smith>.txt".RemoveInvalidPathCharacters());
            Assert.Equal("New Folder\\file:name.doc", "New Folder\\file:name.doc".RemoveInvalidPathCharacters());

            Assert.Equal("", "".RemoveInvalidPathCharacters());
            Assert.Equal(" ", " ".RemoveInvalidPathCharacters());

            Assert.Equal("Filename with spaces/in path.txt", "Filename with spaces/in path.txt".RemoveInvalidPathCharacters());
            Assert.Equal("C:\\Data 100%.xls", "C:\\Data 100%.xls".RemoveInvalidPathCharacters());
            Assert.Equal("C:/Applications/日本語.txt", "C:/Applications/日本語.txt".RemoveInvalidPathCharacters()); // Preserves Japanese characters
        }
    }
}
