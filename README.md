1.
OnInitializedAsync runs once when the component is first created — ideal for one-time setup like fetching initial data.
OnParametersSetAsync runs every time parameters change (including the first time, after OnInitializedAsync).
If a parent passes a new Id parameter, OnParametersSetAsync fires again; OnInitializedAsync does not.

2.
Blazor renders to a virtual DOM first — the actual browser DOM doesn't exist yet during OnInitialized or OnParametersSet.
Only after OnAfterRenderAsync fires is the real DOM available. So anything that needs a real element (JS Interop, focus management,
third-party JS libraries, canvas) must go here. The firstRender parameter lets you guard one-time setup.

3. 
In Blazor Server, a Singleton is shared across all users and all circuits for the lifetime of the application.
This means user A's data can bleed into user B's session — a serious security and correctness issue. Scoped is the right
lifetime for per-user state (one scope per circuit/session). Singleton is only safe for truly global, stateless, or immutable data.

4. 
A typed client wraps HttpClient in a dedicated class with named methods (GetWeatherAsync(), etc.), so you get a clean API,
centralized base URL and header configuration, and easy testability via interface mocking. Calling HttpClient directly in every
component scatters base URLs, error handling, and deserialization logic everywhere — hard to maintain and test.

5. 
NavLink is Blazor's routing-aware anchor. It automatically adds the active CSS class when the current URL matches its href,
which is essential for highlighting active nav items. A plain <a> tag is static HTML with no awareness of Blazor's router — it won't
get the active class and triggers a full page navigation rather than client-side routing.

6.
It's a delegate that represents a parameterized chunk of UI, letting a parent pass templated markup into a component.
The T is context data the component provides back to the template. Classic use case: a generic list or table component where the
parent defines how each row renders — the component handles iteration/layout, the caller handles the per-item appearance.
RenderFragment (non-generic) is the same idea without the context parameter.

7.
Use it when you genuinely need something only the browser/JS ecosystem provides: focus management, clipboard API, third-party JS
libraries (charts, maps), local storage, measuring DOM element dimensions. Avoid it when Blazor can do the job natively — it adds
complexity, breaks the component model, is harder to test, and creates tight coupling to the JS side. Rule of thumb: JS Interop is
an escape hatch, not a first resort.

8. 
ErrorBoundary catches unhandled exceptions thrown during rendering within its subtree and displays a fallback UI instead of crashing
the whole page. This improves resilience — one broken widget doesn't take down the entire app. However, it should not replace proper
error handling (try/catch in async methods, validation, logging). It's a last-resort safety net for unexpected render failures, not
a substitute for writing defensive code.
