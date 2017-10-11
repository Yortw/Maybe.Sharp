# Maybe.Sharp
A generic 'option' type for .Net, explicitly representing a value that is either 'something' or 'nothing.

[![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/Yortw/Maybe.Sharp/blob/master/LICENSE) [![Coverage Status](https://coveralls.io/repos/github/Yortw/Maybe.Sharp/badge.svg?branch=master)](https://coveralls.io/github/Yortw/Maybe.Sharp?branch=master) [![Build status](https://ci.appveyor.com/api/projects/status/waxmch4c6sm96vaa?svg=true)](https://ci.appveyor.com/project/Yortw/maybe-sharp) [![NuGet Badge](https://buildstats.info/nuget/Maybe.Sharp)](https://www.nuget.org/packages/Maybe.Sharp/)

```powershell
    PM> Install-Package Maybe.Sharp
```

# Why ?
Yes there are plenty of maybe/option types for .Net, but I couldn't find one that met all my requirements, hence Maybe.Sharp exists.

# Features
See the [API Documentation](file:///C:/Projects/GitHub/Maybe.Sharp/docs/api/MaybeSharp.Maybe-1.html) for contents/help.

* Struct/Value type 
    * Instances of Maybe<T>  cannot be null

* (In)Equality & GetHashCode overrides/implementation
* \> \< >= <= operations for Maybe<T> and T values.
* Implements IComparable<T>, IComparable<Maybe<T>>, IComparable, IEquatable<Maybe<T>>, IFormattable
* Implict cast from T to Maybe<T>
* Explicit cast from Maybe<T> to T (exception when 'nothing')
* Bind method for piping Maybe<T> through functions
* Chainable WhenSomething/WhenNothing for control flow
* Static Nothing value for each Maybe<T> type.
* Maybe<T> of other value types (not just reference types), i.e Maybe<T> of System.Guid is 'empty'/'nothing when the inner value is System.Guid.Empty. Same for DateTime when the value is DateTime.MinValue etc.
* Cast/As methods for converting Maybe<T> to Maybe<X>
* ToString override
* TryGetValue extension for IDictionary<K, V>
* Or method to Coalesce Maybe<T>
* ValueOr method for retrieving wrapped value or default value when empty
* ToNullable to convert Maybe<T> to a Nullable<T> equivalent
* ToMaybe extension for Nullable<T> values.
* LINQ support via where and select extensions
* TryParse extensions for System.String
