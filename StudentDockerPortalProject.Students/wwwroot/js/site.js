/* ─────────────────────────────────────────────
   Student Portal — GSAP Animation Controller
   ───────────────────────────────────────────── */

document.addEventListener('DOMContentLoaded', function () {

  // ── Register ScrollTrigger ──
  if (typeof ScrollTrigger !== 'undefined') {
    gsap.registerPlugin(ScrollTrigger);
  }

  // ── Navbar shrink on scroll ──
  const nav = document.getElementById('mainNav');
  if (nav) {
    window.addEventListener('scroll', function () {
      nav.classList.toggle('scrolled', window.scrollY > 30);
    });
  }

  // ── Animate page on load ──
  gsap.from('main > .container > *', {
    y: 20,
    opacity: 0,
    duration: 0.5,
    stagger: 0.06,
    ease: 'power2.out',
    clearProps: 'all'
  });

  // ── Stagger table rows ──
  const rows = document.querySelectorAll('.table-row');
  if (rows.length) {
    gsap.from(rows, {
      y: 12,
      opacity: 0,
      duration: 0.35,
      stagger: 0.04,
      ease: 'power1.out',
      clearProps: 'all'
    });
  }

  // ── Stagger stat cards ──
  const stats = document.querySelectorAll('.stat-card');
  if (stats.length) {
    gsap.from(stats, {
      y: 16,
      opacity: 0,
      duration: 0.4,
      stagger: 0.08,
      ease: 'power2.out',
      clearProps: 'all'
    });
  }

  // ── Animate detail cards ──
  const details = document.querySelectorAll('.detail-card');
  if (details.length) {
    gsap.from(details, {
      x: -10,
      opacity: 0,
      duration: 0.35,
      stagger: 0.06,
      ease: 'power1.out',
      clearProps: 'all'
    });
  }

  // ── Hover tilt for cards ──
  const cards = document.querySelectorAll('.card-clean');
  cards.forEach(function (card) {
    card.addEventListener('mousemove', function (e) {
      const rect = card.getBoundingClientRect();
      const x = (e.clientX - rect.left) / rect.width - 0.5;
      const y = (e.clientY - rect.top) / rect.height - 0.5;
      gsap.to(card, { rotationY: x * 3, rotationX: -y * 3, duration: 0.3, ease: 'power1.out' });
    });
    card.addEventListener('mouseleave', function () {
      gsap.to(card, { rotationY: 0, rotationX: 0, duration: 0.4, ease: 'power2.out' });
    });
  });

});
