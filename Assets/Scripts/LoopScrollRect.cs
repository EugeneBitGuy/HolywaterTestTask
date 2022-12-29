using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoopScrollRect : ScrollRect
{
        private bool hasOverflow = false;
        public bool isManualScroll;
    
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            if (hasOverflow)
            {
                OnEndDrag(eventData);
                OnBeginDrag(eventData);
            }
        }
    
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            isManualScroll = false;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            isManualScroll = true;
        }

        protected override void SetContentAnchoredPosition(Vector2 position)
        {
            hasOverflow = false;
            
            var offset = Vector2.zero;
            
            Vector2 min = m_ContentBounds.min;
            Vector2 max = m_ContentBounds.max;


            var rect = viewRect.rect;
            float maxOffset = rect.max.x - max.x;
            float minOffset = rect.min.x - min.x;
    
            if (minOffset < -0.001f)
            {
                offset.x = -minOffset + maxOffset;
                hasOverflow = true;
            }
            else if (maxOffset > 0.001f)
            {
                offset.x = -maxOffset + minOffset;
                hasOverflow = true;
            }
    
            base.SetContentAnchoredPosition(position + offset);
        }
        
        protected override void LateUpdate()
        {
            var vel = velocity;
            base.LateUpdate();
            if (hasOverflow)
            {
                velocity = vel;
            }
            
        }
}
