// @ts-check

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: 'Finmer Editor',
  tagline: 'Furry text adventure engine',
  url: 'https://docs.finmer.dev',
  baseUrl: '/',
  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',
  favicon: 'images/Favicon.png',
  organizationName: 'pileofwolves',
  projectName: 'finmer',
  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },

  presets: [
    [
      'classic',
      {
        docs: {
          routeBasePath: '/',
          path: 'content',
          sidebarPath: require.resolve('./sidebars.js'),
          editUrl:
            'https://github.com/pileofwolves/finmer/edit/master/Docs/',
        },
        blog: false,
        theme: {
          customCss: [require.resolve('./src/css/custom.css')],
        },
      },
    ],
  ],

  scripts: [
    {
      defer: true,
      'data-domain': 'docs.finmer.dev',
      src: 'https://pl.finmer.dev/js/script.js',
    }
  ],

  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      navbar: {
        title: 'Finmer Editor',
        logo: {
          alt: 'Finmer Logo',
          src: 'images/LogoBlack.png',
          srcDark: 'images/LogoWhite.png',
        },
        items: [
          {
            type: 'doc',
            docId: '/category/getting-started',
            position: 'left',
            label: 'Start Here',
          },
          {
            type: 'doc',
            docId: '/category/tutorial',
            position: 'left',
            label: 'Tutorial',
          },
          {
            type: 'doc',
            docId: '/category/assets',
            position: 'left',
            label: 'Asset Reference',
          },
          {
            type: 'doc',
            docId: 'script-reference/index',
            position: 'left',
            label: 'Script Reference',
          },
          {
            href: 'https://finmer.dev',
            label: 'Back to Community',
            position: 'right',
          },
          {
            href: 'https://github.com/pileofwolves/finmer',
            label: 'GitHub',
            position: 'right',
          },
        ],
      },
      footer: {
        style: 'dark',
        links: [
          {
            title: 'Docs',
            items: [
              {
                label: 'Setup Guide',
                to: '/getting-started/editor-setup',
              },
              {
                label: 'Your First Quest',
                to: '/tutorial/preface',
              },
            ],
          },
          {
            title: 'Let\'s chat!',
            items: [
              {
                label: 'Community Site',
                href: 'https://finmer.dev',
              },
              {
                label: 'FurAffinity',
                href: 'https://furaffinity.net/user/nuntis',
              },
            ],
          },
          {
            title: 'Develop',
            items: [
              {
                label: 'GitHub',
                href: 'https://github.com/pileofwolves/finmer',
              },
            ],
          },
        ],
        copyright: `Copyright © ${new Date().getFullYear()} Nuntis the Wolf. Documentation pages built with Docusaurus.`,
      },
      colorMode: {
        defaultMode: 'light',
      },
      prism: {
        theme: require('prism-react-renderer').themes.vsLight,
        darkTheme: require('prism-react-renderer').themes.vsDark,
        additionalLanguages: ['lua'],
      },
    }),

  plugins: [
    [
      "docusaurus-booklet",
      /** @type {import("docusaurus-booklet").BookletPluginOptions} */
      ({
        cover: {
          title: "Finmer Editor",
        },
        selectors: {
          mainContent: "article",
          pagination: ".pagination-nav__link--next",
          sidebar: ".theme-doc-sidebar-menu",
          exclude: ["nav.navbar,footer.footer,.theme-doc-toc-mobile",".card",".breadcrumbs",".theme-edit-this-page"],
        },
      })
    ]
  ]
};

module.exports = config;
