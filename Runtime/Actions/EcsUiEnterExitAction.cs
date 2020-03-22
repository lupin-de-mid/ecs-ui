// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2020 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using Leopotam.Ecs.Ui.Components;
using UnityEngine.EventSystems;

namespace Leopotam.Ecs.Ui.Actions {
    /// <summary>
    /// Ui action for processing enter / exit cursor events.
    /// </summary>
    public sealed class EcsUiEnterExitAction : EcsUiActionBase, IPointerEnterHandler, IPointerExitHandler {
        void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Set<EcsUiEnterEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
            }
        }

        void IPointerExitHandler.OnPointerExit (PointerEventData eventData) {
            if (IsValidForEvent ()) {
                ref var msg = ref Emitter.CreateEntity ().Set<EcsUiExitEvent> ();
                msg.WidgetName = WidgetName;
                msg.Sender = gameObject;
            }
        }
    }
}