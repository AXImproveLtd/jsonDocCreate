# jsonDocCreate
Fast and ultra compact C# class to create well-formed JSON documents in sequential, top-down manner.

There are many cases which fall between simplest, few-element JSON output(possible using StringBuilder or String.Format) and complex ones, where proper serializer is the right choice.

jsonDocCreate has been created to cater for such mid-level cases. It is 10 times quicker than serializers because it uses StringBuilder to instantly append content. The speed comes at a price - document needs to be built top-to-bottom.

## jsonDocCreate requires building the JSON document SEQUENTIALLY.

Code below builds sample JSON from [Wikipedia](https://en.wikipedia.org/wiki/JSON)

```c#
   {
    var doc = JSONElement.JSONDocument.newDoc(true);

    using (var root = doc.addObject())
    {
        root.addString("firstName", "John");
        root.addString("lastName", "Smith");
        root.addBoolean("isAlive", true);
        root.addNumber("age", 27);
        using (var addres = root.addObject("address"))
        {
            addres.addString("streetAddress", "21 2nd Street");
            addres.addString("city", "New York");
            addres.addString("state", "NY");
            addres.addString("postalCode", "10021-3100");
        }
        using (var phoneNumbers = root.addArray("phoneNumbers"))
        {
            using (var phone = phoneNumbers.addObject())
            {
                phone.addString("type", "home");
                phone.addString("number", "212 555-1234");
            }
  [....]           
        }        
    }
    string output = doc.getOutput();
  }
  ```


Internal controls prevent out-of-order additions, ensuring that document is valid.
Serialized content can be retrieved only after all document's backets have been closed (e.g. objects and arrays deallocated in the proper sequence)
