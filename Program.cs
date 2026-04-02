using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using ImGuiNET;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace Chickentendo;

class Program
{
    private static IWindow _window = null!;
    private static GL _gl = null!;
    private static ImGuiController _controller = null!;

    static void Main(string[] args)
    {
        var options = WindowOptions.Default;
        options.Title = "Chickentendo";
        options.Size = new Silk.NET.Maths.Vector2D<int>(1280, 720);
        
        // Key for efficiency: Wait for events instead of constant polling
        options.WindowBorder = WindowBorder.Resizable;

        _window = Window.Create(options);

        _window.Load += OnLoad;
        _window.Render += OnRender;
        _window.Update += OnUpdate;
        _window.Closing += OnClose;

        _window.Run();
    }

    private static void OnLoad()
    {
        _gl = _window.CreateOpenGL();
        _controller = new ImGuiController(_gl, _window, _window.CreateInput());
    }

    private static void OnUpdate(double deltaTime)
    {
        _controller.Update((float)deltaTime);
    }

    private static void OnRender(double deltaTime)
    {
        _gl.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
        _gl.Clear(ClearBufferMask.ColorBufferBit);

        // --- UI DRAWING START ---
        ImGui.Begin("Chickentendo Control Panel");
        ImGui.Text("Mesen 2 Core: Not Connected (Placeholder)");
        
        if (ImGui.Button("Load ROM"))
        {
            // Placeholder for File Picker
        }

        if (ImGui.CollapsingHeader("Legal & About"))
        {
            ImGui.Text("Core: Mesen 2 (GPLv3)");
            ImGui.Text("UI: Dear ImGui (MIT)");
        }
        ImGui.End();
        // --- UI DRAWING END ---

        _controller.Render();
    }

    private static void OnClose()
    {
        _controller?.Dispose();
        _gl?.Dispose();
    }
}