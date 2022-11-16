#pragma once

namespace Project1
{
	public ref class HelloWorld
	{
	public:
		static property System::String^ Salutations { System::String^ get() { return "Hello World! (From C++/CLI)"; } }
	};
}
