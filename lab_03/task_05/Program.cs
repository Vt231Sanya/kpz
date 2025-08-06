using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

abstract class LightNode
{
    public string OuterHTML => GenerateOuterHTML();
    public string InnerHTML => GenerateInnerHTML();

    protected abstract string GenerateOuterHTML();
    protected abstract string GenerateInnerHTML();

   public abstract void Accept(IVisitor visitor); 
}

class LightTextNode : LightNode
{
    public string Text { get; }

    public LightTextNode(string text) => Text = text;

    protected override string GenerateOuterHTML() => Text;
    protected override string GenerateInnerHTML() => Text;

    public override void Accept(IVisitor visitor) => visitor.VisitText(this);
}

enum TagType { SelfClosing, Paired }

class LightElementNode : LightNode
{
    public string TagName { get; }
    public TagType TagKind { get; }
    public List<string> CssClasses { get; } = new List<string>();
    public LightNodeCollection Children { get; } = new LightNodeCollection();

    private IDisplayState _displayState = new BlockState();
    public void SetDisplayState(IDisplayState state) => _displayState = state;
    public string Render() => _displayState.Render(this);

    public LightElementNode(string tagName, TagType tagKind)
    {
        TagName = tagName;
        TagKind = tagKind;
    }

    public void AddClass(string className) => CssClasses.Add(className);

    public void AddChild(LightNode child)
    {
        if (TagKind == TagType.SelfClosing)
            throw new InvalidOperationException("Self-closing tags can't have children.");
        Children.Add(child);
    }

    protected override string GenerateInnerHTML()
    {
        var sb = new StringBuilder();
        foreach (var child in Children)
            sb.Append(child.OuterHTML);
        return sb.ToString();
    }

    protected override string GenerateOuterHTML()
    {
        var classAttr = CssClasses.Count > 0 ? $" class=\"{string.Join(" ", CssClasses)}\"" : "";
        if (TagKind == TagType.SelfClosing)
            return $"<{TagName}{classAttr} />";
        else
            return $"<{TagName}{classAttr}>{InnerHTML}</{TagName}>";
    }

    public override void Accept(IVisitor visitor) => visitor.VisitElement(this);
}

class LightNodeIterator : IEnumerator<LightNode>
{
    private readonly List<LightNode> _nodes;
    private int _position = -1;

    public LightNodeIterator(List<LightNode> nodes) => _nodes = nodes;

    public LightNode Current => _nodes[_position];
    object System.Collections.IEnumerator.Current => Current;
    public bool MoveNext() => ++_position < _nodes.Count;
    public void Reset() => _position = -1;
    public void Dispose() { }
}

class LightNodeCollection : IEnumerable<LightNode>
{
    private readonly List<LightNode> _nodes = new List<LightNode>();
    public void Add(LightNode node) => _nodes.Add(node);
    public IEnumerator<LightNode> GetEnumerator() => new LightNodeIterator(_nodes);
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    public int Count => _nodes.Count;
}

interface ICommand
{
    void Execute();
}

class AddClassCommand : ICommand
{
    private readonly LightElementNode _element;
    private readonly string _className;

    public AddClassCommand(LightElementNode element, string className)
    {
        _element = element;
        _className = className;
    }

    public void Execute() => _element.AddClass(_className);
}

interface IDisplayState
{
    string Render(LightElementNode node);
}

class BlockState : IDisplayState
{
    public string Render(LightElementNode node) => $"<div>{node.InnerHTML}</div>";
}

class InlineState : IDisplayState
{
    public string Render(LightElementNode node) => $"<span>{node.InnerHTML}</span>";
}

interface IVisitor
{
    void VisitText(LightTextNode textNode);
    void VisitElement(LightElementNode elementNode);
}

class TagCountVisitor : IVisitor
{
    public int Count { get; private set; } = 0;

    public void VisitText(LightTextNode textNode) { }

    public void VisitElement(LightElementNode elementNode)
    {
        Count++;
        foreach (var child in elementNode.Children)
            child.Accept(this);
    }
}
class LightElementFactory
{
    private readonly Dictionary<string, LightElementNode> _elements = new Dictionary<string, LightElementNode>();

    public LightElementNode GetElement(string tag)
    {
        if (!_elements.ContainsKey(tag))
            _elements[tag] = new LightElementNode(tag, TagType.Paired);
        return _elements[tag];
    }

    public int UniqueTagCount => _elements.Count;
}
class MemoryCalculatorVisitor : IVisitor
{
    public long TotalMemoryBytes { get; private set; } = 0;

    public void VisitText(LightTextNode textNode)
    {
        TotalMemoryBytes += textNode.Text.Length * sizeof(char);
    }

    public void VisitElement(LightElementNode elementNode)
    {
        TotalMemoryBytes += elementNode.TagName.Length * sizeof(char);
        foreach (var child in elementNode.Children)
            child.Accept(this);
    }
}


class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines("book.txt");
        var root = new LightElementNode("div", TagType.Paired);
        var factory = new LightElementFactory();

        foreach (var line in lines)
        {
            LightElementNode element;
            if (root.Children.Count == 0)
                element = factory.GetElement("h1");
            else if (line.Length < 20)
                element = factory.GetElement("h2");
            else if (line.StartsWith(" "))
                element = factory.GetElement("blockquote");
            else
                element = factory.GetElement("p");

            var node = new LightElementNode(element.TagName, TagType.Paired);
            node.AddChild(new LightTextNode(line));
            root.AddChild(node);
        }

        Console.WriteLine("HTML Output");
        Console.WriteLine(root.OuterHTML);

        var memoryVisitor = new MemoryCalculatorVisitor();
        root.Accept(memoryVisitor);
        Console.WriteLine($"\nMemory used (approx): {memoryVisitor.TotalMemoryBytes} bytes");

        Console.WriteLine($"\nUnique tag objects via Flyweight: {factory.UniqueTagCount}");
    }
}
