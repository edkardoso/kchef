using edk.Tools.NoIf;
using edk.Tools.NoIf.Miscellaneous;

namespace edk.ToolsTest
{
    public class NoIfTest
    {
        #region class Privates

        private class ParentObj
        {
            public bool Test { get; set; }
            public int Value { get; set; }

        }

        private class ChildObj
        {
            public string Result { get; set; } = string.Empty;
        }

        private class GrandObj
        {
            public string Result { get; set; } = string.Empty;
        }
        #endregion


        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, -1)]
        public void If_FuncBoolean_ShouldReturnYesWhenTrueAndNotWhenFalse(bool test, int expected)
        {
            // arrange
            ParentObj parent = new() { Test = test };
            Func<bool> func = () => parent.Test;

            //action
            func.If(() => parent.Value = 1, () => parent.Value = -1);

            // assert
            Assert.Equal(expected, parent.Value);

        }


        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, -1)]
        public void If_Boolean_ShouldReturnYesWhenTrueAndNotWhenFalse(bool test, int expected)
        {
            // arrange
            ParentObj parent = new() { Test = test };

            //action
            parent.Test.If(() => parent.Value = 1, () => parent.Value = -1);

            // assert
            Assert.Equal(expected, parent.Value);

        }



        [Theory]
        [InlineData(true, "yes")]
        [InlineData(false, "no")]
        public void If_Generic_ShouldReturnYesWhenTrueAndNotWhenFalse(bool test, string expected)
        {
            // arrange
            ParentObj parent = new() { Test = test };


            // action
            var result = parent
                .If(condition: o => o.Test,
                whenTrue: (o) => "yes",
                whenFalse: (o) => "no");

            // assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void If_Generic_MustChangeValueProperty()
        {
            // arrange
            var initialValue = 1;
            var newValue = 100;
            ParentObj parent = new() { Test = true, Value = initialValue };

            // action
            parent
                 .If(condition: o => o.Test,
                     whenTrue: (o) => o.Value = newValue,
                     whenFalse: (o) => o.Value = initialValue);

            // assert
            Assert.Equal(newValue, parent.Value);

        }

        [Fact]
        public void If_Generic_ShouldCreateInstanceGrandObjWhenChildObjEqualYes()
        {
            // arrange
            ParentObj parent = new() { Test = true };

            // action
            GrandObj expected = parent
                  .If<ParentObj, ChildObj>(condition: o => o.Test,
                      whenTrue: (o) => new() { Result = "yes" },
                      whenFalse: (o) => new() { Result = "No" })
                  .If<ChildObj, GrandObj>(condition: o => o.Result.Equals("yes"),
                      whenTrue: (o) => new(),
                      whenFalse: (o) => null);

            // assert
            Assert.NotNull(expected);

        }

        [Fact]
        public void If_Generic_ShouldReturnNullWhenChildObjEqualNo()
        {
            // arrange
            ParentObj parent = new() { Test = true };

            // action
            GrandObj expected = parent
                  .If<ParentObj, ChildObj>(condition: o => o.Test.IsFalse(),
                  whenTrue: (o) => new() { Result = "yes" },
                  whenFalse: (o) => new() { Result = "No" })
                  .If<ChildObj, GrandObj>(condition: o => o.Result.Equals("yes"),
                  whenTrue: (o) => new(),
                  whenFalse: (o) => null);

            // assert
            Assert.Null(expected);

        }
    }
}